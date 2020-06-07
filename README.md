# Midi Parser in C#

A small midi parser written in C#. Just drop [`MidiFile.cs`](MidiFile.cs) into your project and use it like:

```c#
var midiFile = new MidiFile("song.mid");

// 0 = single-track, 1 = multi-track, 2 = multi-pattern
var midiFileformat = midiFile.Format;

// also known as pulses per quarter note
var ticksPerQuarterNote = midiFile.TicksPerQuarterNote;

foreach(var track in midiFile.Tracks)
{
    foreach(var midiEvent in track.MidiEvents)
    {
        if(midiEvent.MidiEventType == MidiEventType.NoteOn)
        {
            var channel = midiEvent.Channel;
            var note = midiEvent.Note;
            var velocity = midiEvent.Velocity;
        }
    }

    foreach(var textEvent in track.TextEvents)
    {
        if(textEvent.TextEventType == TextEventType.Lyric)
        {
            var time = textEvent.Time;
            var text = textEvent.Value;
        }
    }    
}
```

# Notes

- Extracts channel number from the event type for easier use.
- Provides easy access to events relevant to music performance like tempo and time signature.
- The code is easy to extend to support other event types.

Some implementation details:

- Events are stored without polymorphism using a small shared MidiEvent struct.
- Text events are stored separately because of their reference to strings.
- The parsing is done using byte arrays instead of streams since midi files are so small.
- Only works in one direction so it can't write midi files.

Some event types are preprocessed during the parsing:

- Note on events with zero velocity are normalized to note off events.
- Tempo events are simplified to beats per minute.
- Time signature events are simplified to standard notation.

## Midi File

```c#
class MidiFile
{
    int Format;
    int TicksPerQuarterNote;
    MidiTrack[] Tracks;
}
```

```c#
class MidiTrack
{
    int Index;
    List<MidiEvent> MidiEvents;
    List<TextEvent> TextEvents;
}
```

## Midi Events

```c#
struct MidiEvent
{
    int Time;
    byte Type;
    byte Arg1;
    byte Arg2;
    byte Arg3;
}
```

| Type                | Arg1            | Arg2         | Arg3        |
| ------------------- | --------------- | -----------  | ----------- |
| *NoteOff*           | Channel         | Note         | Velocity    |
| *NoteOn*            | Channel         | Note         | Velocity    |
| *PitchBendChange*   | Channel         | Value        | Value       |
| *KeyAfterTouch*     | Channel         | Note         | Amount      |
| *ChannelAfterTouch* | Channel         | Amount       | -           |
| *ProgramChange*     | Channel         | Program      | -           |
| *ControlChange*     | Channel         | *BankSelect* | Value       |
|                     | Channel         | *Modulation* | Value       |
|                     | Channel         | *Volume*     | Value       |
|                     | Channel         | *Balance*    | Value       |
|                     | Channel         | *Pan*        | Value       |
|                     | Channel         | *Sustain*    | Value       |
| *MetaEvent*         | *Tempo*         | BeatsMinute  | -           |
|                     | *TimeSignature* | Numerator    | Denominator |
|                     | *KeySignature*  | SharpsFlats  | MajorMinor  |

## Text Events

```c#
struct TextEvent
{
    int Time;
    byte Type;
    string Value;
}
```

| Type        |
| ----------- |
| *Text*      |
| *TrackName* |
| *Lyric*     |

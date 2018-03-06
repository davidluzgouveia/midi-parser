# Midi Parser in C#

- Must separate event types from channel for easier handling.
- Must be able to iterate midi events without polymorphism.
- Provide easy access to events relevant to music performance, such as tempo and time signature.
- TextEvents are stored separately so that each regular event can fit in 64 bytes.
- NoteOn events with 0 velocity get normalized to NoteOff events.
- Tempo events are simplified, e.g. 120bpm.
- Time signature events are simplified, e.g. 3/4.


## Midi File


```c#
class MidiFile
{
    int TicksPerQuarterNote;
    MidiTrack[] Tracks;
}
```

```c#
class MidiTrack
{
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

## Example Usage

```c#
var midiFile = new MidiFile("song.mid");

foreach(var track in midiFile.Tracks)
{
    foreach(var midiEvent in track.MidiEvents)
    {
        if(midiEvent.MidiEventType == MidiEventType.NoteOn)
        {
            var time = midiEvent.Time;
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
namespace MidiFileTests
{
    using System;

    using MidiParser.Helper;

    public static class Program
    {
        private static void Main()
        {
            var midiFile = MidiFileHelper.FromFile("test.mid");

            Console.WriteLine("TicksPerQuarterNote: " + midiFile.TicksPerQuarterNote);

            foreach (var track in midiFile.Tracks)
            {
                Console.WriteLine("Track: " + track.Index);

                foreach (var midiEvent in track.MidiEvents)
                {
                    Console.WriteLine(
                        " - Command: " + (MidiEventType)midiEvent.Type + " / Channel: " + midiEvent.Arg1 + " / Time: "
                        + midiEvent.Time + " / Byte1: " + midiEvent.Arg2 + " / Byte2: " + midiEvent.Arg3);
                }
            }
        }
    }
}
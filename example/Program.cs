namespace MidiFileTests
{
    using System;

    using MidiParser;

    public static class Program
    {
        private static void Main()
        {
            const string Path = "test.mid";

            Console.WriteLine("Parsing: {0}\n", Path);

            var midiFile = new MidiFile(Path);

            Console.WriteLine("Format: {0}", midiFile.Format);
            Console.WriteLine("TicksPerQuarterNote: {0}", midiFile.TicksPerQuarterNote);
            Console.WriteLine("TracksCount: {0}", midiFile.TracksCount);

            foreach (var track in midiFile.Tracks)
            {
                Console.WriteLine("\nTrack: {0}\n", track.Index);

                foreach (var midiEvent in track.MidiEvents)
                {
                    const string Format = "{0} Channel {1} Time {2} Args {3} {4}";
                    if (midiEvent.MidiEventType == MidiEventType.MetaEvent)
                    {
                        Console.WriteLine(
                            Format,
                            midiEvent.MetaEventType,
                            "-",
                            midiEvent.Time,
                            midiEvent.Arg2,
                            midiEvent.Arg3);
                    }
                    else
                    {
                        Console.WriteLine(
                            Format,
                            midiEvent.MidiEventType,
                            midiEvent.Channel,
                            midiEvent.Time,
                            midiEvent.Arg2,
                            midiEvent.Arg3);
                    }
                }
            }

            Console.WriteLine();
        }
    }
}
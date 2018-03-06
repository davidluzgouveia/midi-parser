namespace MidiParser.Helper
{
    using System.IO;

    public enum MidiEventType : byte
    {
        NoteOff = 0x80,

        NoteOn = 0x90,

        KeyAfterTouch = 0xA0,

        ControlChange = 0xB0,

        ProgramChange = 0xC0,

        ChannelAfterTouch = 0xD0,

        PitchBendChange = 0xE0,

        MetaEvent = 0xFF
    }

    public enum ControlChangeType : byte
    {
        BankSelect = 0x00,

        Modulation = 0x01,

        Volume = 0x07,

        Balance = 0x08,

        Pan = 0x0A,

        Sustain = 0x40
    }

    public enum TextEventType : byte
    {
        Text = 0x01,

        TrackName = 0x03,

        Lyric = 0x05,
    }

    public enum MetaEventType : byte
    {
        Tempo = 0x51,

        TimeSignature = 0x58,

        KeySignature = 0x59
    }

    public static class MidiFileHelper
    {
        public static int Channel(this MidiEvent midiEvent)
        {
            return midiEvent.Arg1;
        }

        public static ControlChangeType ControlChangeType(this MidiEvent midiEvent)
        {
            return (ControlChangeType)midiEvent.Arg2;
        }

        public static MidiFile FromFile(string path)
        {
            return new MidiFile(File.ReadAllBytes(path));
        }

        public static MidiFile FromStream(Stream stream)
        {
            return new MidiFile(ReadAllBytesFromStream(stream));
        }

        public static MetaEventType MetaEventType(this MidiEvent midiEvent)
        {
            return (MetaEventType)midiEvent.Arg1;
        }

        public static MidiEventType MidiEventType(this MidiEvent midiEvent)
        {
            return (MidiEventType)midiEvent.Type;
        }

        public static byte Note(this MidiEvent midiEvent)
        {
            return midiEvent.Arg2;
        }

        public static TextEventType TextEventType(this TextEvent textEvent)
        {
            return (TextEventType)textEvent.Type;
        }

        public static byte Velocity(this MidiEvent midiEvent)
        {
            return midiEvent.Arg3;
        }

        private static byte[] ReadAllBytesFromStream(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}
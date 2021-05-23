namespace dotEmu.emulators.NES
{
    class NESEmulator : IEmulator
    {
        public string Name => "NES";

        public Bus mainBus;

        public NESEmulator()
        {
            mainBus = new Bus();
        }
    }
}

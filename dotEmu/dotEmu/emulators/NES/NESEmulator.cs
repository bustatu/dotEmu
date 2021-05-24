/*
 * Resources used:
 * https://github.com/OneLoneCoder/olcNES
 * https://github.com/Xyene/Emulator.NES
 */

namespace dotEmu.emulators.NES
{
    class NESEmulator : IEmulator
    {
        public string Name => "NES";

        public Bus mainBus;
        public Cartridge cart;

        public NESEmulator()
        {
            // Load cartridge
            cart = new Cartridge("nestest.nes");

            // Create bus
            mainBus = new Bus();

            // Insert cartridge
            mainBus.insertCartridge(cart);

            // Reset system
            mainBus.reset();
        }
    }
}

namespace CoinTowerIdle.Core
{
    public readonly struct GameTick
    {
        public readonly int TickNumber;

        public readonly float DeltaTime;

        public GameTick(int tickNumber, float deltaTime)
        {
            TickNumber = tickNumber;
            DeltaTime = deltaTime;
        }
    }
}
using BaseGame.Actors.Pawns;

namespace BaseGame.Drivers
{
    public interface IDriver
    {
    }
    public interface IDriver<T>:IDriver
    {
        void Update(T pawn, float time);
    }
}

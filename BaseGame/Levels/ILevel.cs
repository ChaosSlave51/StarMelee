using BaseGame.Resources;
namespace BaseGame.Levels
{
    public interface ILevel:INeedsResources
    {
        void Update(float time);
    }
}

using BaseGame.Resources;

namespace StarMelee.Actors
{
    internal interface IWeapon:INeedsResources
    {
        void Fire();
        void Update();
    }
}
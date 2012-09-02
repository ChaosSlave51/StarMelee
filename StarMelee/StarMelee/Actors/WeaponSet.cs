using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Actors;
using BaseGame.Resources;

namespace StarMelee.Actors
{
    class WeaponSet : BaseActor, IWeapon
    {
        public int CooldownTime = 0;
        protected int Cooldown = 0;
        public List<IWeapon> Weapons { get; set; }

        public WeaponSet(List<IWeapon> weapons= null )
        {
            if(weapons!=null)
            {
                Weapons = weapons;
            }
            else
            {
                Weapons= new List<IWeapon>();
            }
        }


        public void Fire()
        {
            if(PreFire!=null)
            {
                if(!PreFire())
                {
                    return;
                }
            }

            if (Cooldown == 0)
            {
                Weapons.ForEach(x=>x.Fire());
                Cooldown = CooldownTime;
                if(Fired!=null)
                {
                    Fired();
                }
            }
        }
        public void Update()
        {
            if (Cooldown > 0)
            {
                Cooldown--;
            }
        }

    

        public IEnumerable<Resource> ResourcePaths()
        {
            return Resource.Combine(Weapons);
        }

        protected override void ResolveResources()
        {
            
        }
        
        public event Func<bool> PreFire;
        public event Action Fired;
    }
}

using BaseGame.Inputs;

namespace StarMelee.Inputs
{
    class ActionState:IActionState
    {
        public IInputState Left { get; set; }
        public IInputState Right { get; set; }
        public IInputState Up { get; set; }
        public IInputState Down { get; set; }
        public IInputState Fire1 { get; set; }
        public IInputState Fire2 { get; set; }
        public IInputState Fire3 { get; set; }
        public IInputState Fire4 { get; set; }


    }


}

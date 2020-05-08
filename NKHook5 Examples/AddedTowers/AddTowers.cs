using NKHook5.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddedTowers
{
    public class AddTowers : NKPlugin
    {
        //Constructor for our plugin. Stores data like its name & a description of what it does.
        public AddTowers() : base("Added Towers", "Injects 4 new tower types into the game using NKHook5")
        {
        }
        //When a plugin is loaded, NKHook5 will call this function first.
        public override unsafe void onEnable()
        {
            //Removes tower locking from the game, meaning you can have all the towers at rank 1 without having to unlock them all.
            //This may be useful if your tower is locked and you are unsure why. Can help with debugging your mod.
            Patch.removeTowerLockPatch();
            //Injects a new flag into the game, this can be anything but in this example its a tower
            //The corresponding file would be DRIP.tower inside tower definitions.
            //This would also be the tower's TypeName & FactoryName for the tower file and selection menu
            //In the future this will be able to access files like DRIP.bloon too, so better not have name conflicts!
            Patch.injectFlag("DRIP");
            Patch.injectFlag("Flme");
            Patch.injectFlag("0002");
            Patch.injectFlag("Funk");
        }
    }
}

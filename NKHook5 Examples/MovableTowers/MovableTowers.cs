using NKHook5.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static NKHook5.API.RootAPI;

namespace MovableTowers
{
    public class MovableTowers : NKPlugin
    {
        //Constructor for our plugin. Stores data like its name & a description of what it does.
        public MovableTowers():base("Movable Towers", "Press Q to move a selected tower")
        {

        }
        //When a plugin is loaded, NKHook5 will call this function first.
        public override unsafe void onEnable()
        {
            //Call the base function
            base.onEnable();
            //Create some variables for our plugin
            char lastKey = (char)0x0;
            bool processedKey = true;
            bool moving = false;
            //Register a key press event
            //The code inside will be called every key press
            Hooks.KeypressedEvent += (object send, Hooks.KeypressedArgs e) =>
            {
                //Store stuff into our variables
                lastKey = e.key;
                processedKey = false;
            };
            //Main loop for our plugin
            while (true)
            {
                //Make sure CGameSystemPointers isnt null, if it is and we try to use it, the game will crash!
                if (RootAPI.gameBase->CGameSystemPointers != null)
                {
                    //Store winInput & towerManager to save ourselves from typing it over and over
                    CTowerManager* towerManager = RootAPI.gameBase->CGameSystemPointers->CTowerManager;
                    WinInput* winInput = RootAPI.gameBase->CGameSystemPointers->WinInput;
                    //make sure it aint null
                    if (towerManager != null)
                    {
                        //Make sure the tower list isnt null
                        if (towerManager->towers != null)
                        {
                            //Get the last tower in the vector/list
                            CBaseTower* last = towerManager->final[0];
                            //make a value for tracking our loop
                            int inc = 0;
                            //Begin loop. Here we are basically looping through every tower.
                            while (true)
                            {
                                //Get the current tower
                                CBaseTower* currentTower = towerManager->towers[inc];
                                //Make sure it isnt null
                                if (currentTower != null)
                                {
                                    //If its the last tower we gotta break the loop because we're done
                                    if (last == currentTower)
                                    {
                                        break;
                                    }
                                    //if the tower is selected
                                    if (currentTower->IsSellected)
                                    {
                                        //did we process the key?
                                        if (!processedKey)
                                        {
                                            //is Q pressed? (Q toggles moving or not)
                                            if (lastKey == 'Q' || lastKey == 'q')
                                            {
                                                //We are moving? stop!
                                                if (moving)
                                                {
                                                    moving = false;
                                                }
                                                //We're not? lets go!
                                                else
                                                {
                                                    moving = true;
                                                }
                                                //This could be simplified to "moving = !moving" but for simplicity i used the long way
                                            }
                                            processedKey = true;
                                        }
                                        //Are we supposed to move the tower?
                                        if (moving)
                                        {
                                            //set the tower position to our mouse position
                                            currentTower->PosX = winInput->mousePosX;
                                            currentTower->PosY = winInput->mousePosY;
                                        }
                                    }
                                }
                                //Our tower was null so to be safe we break the loop
                                else
                                {
                                    break;
                                }
                                //keep count of towers
                                inc++;
                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}

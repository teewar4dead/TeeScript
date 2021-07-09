﻿using System;
using System.Linq;

using Divine.Entity.Entities.Abilities.Items.Components;
using Divine.Entity.Entities.Units.Components;
using Divine.Extensions;
using Divine.Renderer;

namespace TeeLoneDruid.Bear
{
    class CourierDeliver : GetEntity
    {

        private static readonly float scaling = RendererManager.Scaling;

        [Obsolete]
        public CourierDeliver()
        {
            var countSlot = BearHero.Inventory.FreeMainSlots.Count() + BearHero.Inventory.FreeBackpackSlots.Count();
            Prioritet = true;
            try
            {
               
                if (BearHero.IsAlive && GetCourier().IsAlive && countSlot != 0)
                {
                    
                    if (Prioritet == true)
                    {
                        if (GetCourier().State == CourierState.Deliver && GetSelectDruid() != null)
                        {

                            Prioritet = false;
                        }
                        else
                        {

                          Prioritet = true;
                        }
                    }


                    if (GetCourier().State == CourierState.Deliver && GetSelectBear() != null && Prioritet == true)
                    {
                        CourierGiveItem = true;
                    }
                    if (CourierGiveItem == true)
                    {
                        if (GetCourier().State != CourierState.Move)
                        {
                            if (GetCourier().Distance2D(BearHero.Position) >= 200)
                            {
                                GetCourier().Follow(BearHero);
                               
                            }
                            else
                            {
                                countSlot = BearHero.Inventory.FreeMainSlots.Count() + BearHero.Inventory.FreeBackpackSlots.Count();
                                
                                    for (int i = 0; i <= countSlot; i++)
                                    {
                                        var item = GetCourier().Inventory.GetItem((ItemSlot)i);

                                        GetCourier().Give(item, BearHero);
                                    }
                                
                                GetCourier().Spellbook.Spell3.Cast();
                                CourierGiveItem = false;

                            }
                        }
                        else
                        {
                            CourierGiveItem = false;
                        }
                    }
                }

            }
            catch
            {
                // Ignore
            }
        }
    }
}
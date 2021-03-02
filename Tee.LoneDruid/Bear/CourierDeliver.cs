using Divine;
using Divine.SDK.Extensions;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeeLoneDruid.Bear
{
    class CourierDeliver : GetEntity
    {

        private static readonly float scaling = RendererManager.Scaling;

        [Obsolete]
        public CourierDeliver()
        {
            Prioritet = true;
            try
            {
                Console.WriteLine(GetCourier().State);
                if (BearHero.IsAlive)
                {
                    if (Prioritet == true)
                    {
                        if (GetCourier().State == CourierState.Deliver && GetSelectDruid() != null)
                        {
                            Console.WriteLine("dsd1");
                            Prioritet = false;
                        }
                        else
                        {
                            Console.WriteLine("dsd2");
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
                                foreach (var item in GetCourier().Inventory.Items)
                                {
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
            catch (Exception)
            {


            }


        }


    }
}

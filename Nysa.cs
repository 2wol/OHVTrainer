using System;
using System.Collections.Generic;
using System.Text;
using NWH.VehiclePhysics2.Powertrain;

namespace OHVTrainer
{
    internal class Nysa
    {
        public EngineComponent GetNysaEngine(NysaCarController nysa)
        {
            return nysa._vehicleController.powertrain.engine;
        }
    }
}

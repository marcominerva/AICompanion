using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AICompanion.Services
{
    public interface ISettingsService
    {
        string VisionSubscriptionKey { get; set; }

        string FaceSubscriptionKey { get; set; }

        string CustomVisionPredictionKey { get; set; }

        string CustomVisionProjectId { get; set;  }
    }
}

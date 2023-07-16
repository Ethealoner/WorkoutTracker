using System.Runtime.Serialization;

namespace WorkoutTrackerMvc.Models.ChartsViewModels
{
    [DataContract]
    public class ChartViewModel
    {
        [DataMember(Name = "X")]
        public double X { get; set; }
        [DataMember(Name = "Y")]
        public DateTime Y { get; set; }
    }
}

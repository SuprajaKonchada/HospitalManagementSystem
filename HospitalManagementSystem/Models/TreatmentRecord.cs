using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models;

public partial class TreatmentRecord
{
    public int RecordId { get; set; }

    public int PatientId { get; set; }

    public string TreatmentType { get; set; } = null!;

    public DateTime TreatmentDate { get; set; }

    public string Outcome { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}

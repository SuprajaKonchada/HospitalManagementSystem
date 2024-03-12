using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models;

public partial class MedicalHistory
{
    public int HistoryId { get; set; }

    public int PatientId { get; set; }

    public string MedicalCondition { get; set; } = null!;

    public DateTime DiagnosisDate { get; set; }

    public string Treatment { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}

﻿using System.ComponentModel.DataAnnotations;
using WEBAPI.Models;

namespace WEBAPI.ViewModels.Educations
{
    public class EducationVM
    {
       public Guid? Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        [Range(0,4, ErrorMessage = "Input a Valid number")]
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public static EducationVM ToVM(Education education)
        {
            return new EducationVM
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid
            };
        }
    }
}

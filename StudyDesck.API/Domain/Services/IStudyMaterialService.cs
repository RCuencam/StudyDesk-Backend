﻿using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IStudyMaterialService
    {
        Task<IEnumerable<StudyMaterial>> ListAsync();
        Task<StudyMaterialResponse> GetByIdAsync(int id);
        Task<StudyMaterialResponse> SaveAsync(StudyMaterial studyMaterial);
        Task<StudyMaterialResponse> UpdateAsync(int id, StudyMaterial studyMaterial);
        Task<StudyMaterialResponse> DeleteAsync(int id);
    }
}
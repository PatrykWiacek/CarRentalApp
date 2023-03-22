﻿using CarRental.DAL.Models;
using CarRental.Logic.Models;

namespace CarRental.Logic.Services.IServices;

public interface ISearchService
{
    List<CarModel> SearchList(SearchBLL search);
    List<CarModel> FilterList(SearchBLL searchDto);
}

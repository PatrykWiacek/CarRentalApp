﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using CarRental.Logic.Models;
using CarRental.Logic.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Logic.Services;
public class ReportApiService : IReportApiService
{
    private readonly IMapper _mapper;
    private readonly IRepository<VisitedCar> _visitedCarRepository;
    private readonly IRepository<LastLoggedReport> _lastLoggedReportRepository;

    public ReportApiService(IMapper mapper, IRepository<VisitedCar> visitedCarRepository, IRepository<LastLoggedReport> lastLoggedReportRepository)
    {
        _mapper = mapper;
        _visitedCarRepository = visitedCarRepository;
        _lastLoggedReportRepository = lastLoggedReportRepository;
    }

    public async Task CreateVisitedCarAsync(VisitedCarViewModel model)
    {
        var visitedCar = _mapper.Map<VisitedCar>(model);
        _visitedCarRepository.Insert(visitedCar);
    }

    public async Task CreateLastLoggedAsync(LastLoggedReportDTO model)
    {
        var lastLogged = _mapper.Map<LastLoggedReport>(model);
        _lastLoggedReportRepository.Insert(lastLogged);
        var user = _lastLoggedReportRepository.Get(model.UserId);

        if (user != null)
        {
            user.LastLogged = model.LastLogged;
            user.LoginCount += 1;

             _lastLoggedReportRepository.Update(user);
        }
        else
        {
            //Do ogarniecia
        }
    }

    public async Task<IEnumerable<VisitedCarViewModel>> GetVisitedCarByIdAndDateAsync(int id, DateTime from, DateTime to)
    {
        var lista =  _visitedCarRepository.GetAll().Where(x =>
            x.UserId == id && x.DateWhenClicked >= from && x.DateWhenClicked <= to).ToList();

        return _mapper.Map<IEnumerable<VisitedCarViewModel>>(lista);
    }

    public async Task<IEnumerable<LastLoggedReportDTO>> GetLastLoggerdByIdAndDateAsync(int id, DateTime from, DateTime to)
    {
        var lista =  _lastLoggedReportRepository.GetAll().Where(x =>
            x.UserId == id && x.Created >= from && x.Created <= to).ToList();

        return _mapper.Map<IEnumerable<LastLoggedReportDTO>>(lista);
    }
}
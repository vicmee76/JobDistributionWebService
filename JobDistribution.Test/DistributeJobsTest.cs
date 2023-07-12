using FluentAssertions;
using JobDistributionWebService.Controllers;
using JobDistributionWebService.Helpers;
using JobDistributionWebService.Interfaces;
using JobDistributionWebService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace JobDistribution.Test
{
    public class DistributeJobsTest
    {
        GenerateModels generate = new();
        JobDistributionWebService.Services.JobDistribution _sut;

        public DistributeJobsTest()
        {
            _sut = new JobDistributionWebService.Services.JobDistribution();
        }

        [Fact]
        public async Task Return_Error01_And_Empty_List_Of_Staff_If_Job_List_Is_EmptyAsync____2__Point() 
        {
            //Act
            JobResponse response = new()
            {
                responseCode = "01",
                responseDescription = "Empty job ids or staff list",
                staffWithJobs = new List<Staff>()
            };

            var staff = generate.GenerateStaffList(0);
            var jobs = generate.GenerateJobIds(0);

            var objResultValue =  await _sut.DistributJobsInterface(staff, jobs, "BO");

            objResultValue.Should().BeEquivalentTo(response, x => x.ComparingRecordsByValue().ComparingByMembers<JobResponse>());

            objResultValue.responseCode.Should().Be("01");
            objResultValue.staffWithJobs.Should().BeEmpty();
            objResultValue.responseDescription.Should().NotBeNullOrEmpty();
            objResultValue.staffWithJobs.Should().BeOfType<List<Staff>>();
        }



        [Fact]
        public async Task Return_Error01_And_Empty_List_Of_Staff_If_Staff_List_List_Is_EmptyAsync___2__Point()
        {
            //Act
            JobResponse response = new()
            {
                responseCode = "01",
                responseDescription = "Empty job ids or staff list",
                staffWithJobs = new List<Staff>()
            };

            var staff = generate.GenerateStaffList(0);
            var jobs = generate.GenerateJobIds(0);

            var objResultValue = await _sut.DistributJobsInterface(staff, jobs, "BO");

            objResultValue.Should().BeEquivalentTo(response, x => x.ComparingRecordsByValue().ComparingByMembers<JobResponse>());

            objResultValue.responseCode.Should().Be("01");
            objResultValue.staffWithJobs.Should().BeEmpty();
            objResultValue.responseDescription.Should().NotBeNullOrEmpty();
            objResultValue.staffWithJobs.Should().BeOfType<List<Staff>>();
        }



        [Fact]
        public async Task Return_Error01_When_Staff_With_Desire_Role_Was_NotFound____2__Point()
        {
            //Act
            JobResponse response = new()
            {
                responseCode = "01",
                responseDescription = "Staff with desire role was not found",
                staffWithJobs = new List<Staff>()
            };

            var staff = generate.GenerateStaffList(10);
            var jobs = generate.GenerateJobIds(10);

            var objResultValue = await _sut.DistributJobsInterface(staff, jobs, "XXX");

            objResultValue.Should().BeEquivalentTo(response, x => x.ComparingRecordsByValue().ComparingByMembers<JobResponse>());

            objResultValue.responseCode.Should().Be("01");
            objResultValue.staffWithJobs.Should().BeEmpty();
            objResultValue.responseDescription.Should().NotBeNullOrEmpty();
            objResultValue.staffWithJobs.Should().BeOfType<List<Staff>>();
        }


        [Fact]
        public async Task Return_00_When_Check_If_All_Staf_Are_Free_And_Reurn_The_First_Staff____2__Point()
        {
            var staff = generate.GenerateStaffList(5);
            var jobs = generate.GenerateJobIds(1);

            // make all staff free 

            foreach (var s in staff)
            {
                s.WorkStatus = "Free";
                s.Role = "bo";
            }
                

            //Act
            JobResponse response = new()
            {
                responseCode = "00",
                responseDescription = "First staff -- All staff are free",
                staffWithJobs = new List<Staff>() { staff.FirstOrDefault()}
            };

            var objResultValue = await _sut.DistributJobsInterface(staff, jobs, "BO");

            objResultValue.Should().BeEquivalentTo(response, x => x.ComparingRecordsByValue().ComparingByMembers<JobResponse>());

            objResultValue.responseCode.Should().Be("00");
            objResultValue.staffWithJobs.Should().NotBeNullOrEmpty();
            objResultValue.responseDescription.Should().NotBeNullOrEmpty();
            objResultValue.staffWithJobs.Should().BeOfType<List<Staff>>();
            objResultValue.staffWithJobs.Should().HaveCount(1);
            objResultValue.staffWithJobs.Should().BeEquivalentTo(response.staffWithJobs);
        }




        [Fact]
        public async Task Return_00_When_All_Next_Staff_Is_Found____5__Points()
        {
            var staff = generate.GenerateStaffList(5);
            var jobs = generate.GenerateJobIds(2);

            // make all staff free 

            foreach (var s in staff)
            {
                s.WorkStatus = "Free";
                s.Role = "bo";
            }

            //Act
            JobResponse response = new()
            {
                responseCode = "00",
                responseDescription = "Next Staff",
                staffWithJobs = staff.Take(jobs.Count()).ToList()
            };

            var objResultValue = await _sut.DistributJobsInterface(staff, jobs, "BO");

            objResultValue.Should().BeEquivalentTo(response, x => x.ComparingRecordsByValue().ComparingByMembers<JobResponse>());

            objResultValue.responseCode.Should().Be("00");
            objResultValue.staffWithJobs.Should().NotBeNullOrEmpty();
            objResultValue.responseDescription.Should().NotBeNullOrEmpty();
            objResultValue.staffWithJobs.Should().BeOfType<List<Staff>>();
            objResultValue.staffWithJobs.Should().HaveCount(jobs.Count);
            objResultValue.staffWithJobs.Should().BeEquivalentTo(response.staffWithJobs);
        }



        [Fact]
        public async Task Return_00_When_All_Next_Staff_Is_Found_And_Jobs_Is_More_Than_The_Number_Of_Staff____7__Point()
        {
            var staff = generate.GenerateStaffList(5);
            var jobs = generate.GenerateJobIds(10);

            // make all staff free 

            foreach (var s in staff)
            {
                s.WorkStatus = "Free";
                s.Role = "bo";
            }

            //Act
            JobResponse response = new()
            {
                responseCode = "00",
                responseDescription = "Next Staff",
                staffWithJobs = staff
            };

            var objResultValue = await _sut.DistributJobsInterface(staff, jobs, "BO");

            objResultValue.Should().BeEquivalentTo(response, x => x.ComparingRecordsByValue().ComparingByMembers<JobResponse>());

            objResultValue.responseCode.Should().Be("00");
            objResultValue.staffWithJobs.Should().NotBeNullOrEmpty();
            objResultValue.responseDescription.Should().NotBeNullOrEmpty();
            objResultValue.staffWithJobs.Should().BeOfType<List<Staff>>();
            objResultValue.staffWithJobs.Should().HaveCount(staff.Count);
            objResultValue.staffWithJobs.Sum(x => x.JobOnDesk).Should().Be(jobs.Count);
        }






    }
}

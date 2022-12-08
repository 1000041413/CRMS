import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AuthcredentialstoreService } from '../core/authentication/authcredentialstore.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent implements OnInit {
  userid: number = 0;
  bookings: any=[];

  upcomingBookings: any=[];
  cancelledBooking: any=[];
  bookingHistory: any=[];

  isFacultyManager=false;

  constructor(private http: HttpClient, private router: Router, private authstore: AuthcredentialstoreService) { }

  ngOnInit() {
    this.isFacultyManager = this.authstore.isFacultyManager();
    this.userid = this.authstore.credentials!.userid;

    this.upcomingBookings = [];
    this.cancelledBooking = [];
    this.bookingHistory = [];


    if (this.isFacultyManager) {
      this.loadFacilityManagerDashboard();
    } else {
      this.loadEmployeeDashboard();
    }
  }

  loadFacilityManagerDashboard() {
    this.http
      .get(environment.serverUrl + 'booking/GetCurrentMonthBookings')
      .subscribe((data) => {
        this.bookings = data;
        this.applyfilters(data);
      });
  }

  loadEmployeeDashboard() {
    this.http
      .get(environment.serverUrl + 'booking/GetCurrentMonthBookings?userId=' + this.userid)
      .subscribe((data) => {
        this.bookings = data;
        this.applyfilters(data);
      });
  }

  applyfilters(allbookings:any) {
    console.log(allbookings);
    this.upcomingBookings = allbookings.filter((d:any) => new Date(d.startDateTs) > new Date() && !d.isCancelled);
    this.cancelledBooking = allbookings.filter((d:any) => d.isCancelled);
    this.bookingHistory = allbookings.filter((d:any) => new Date(d.startDateTs) < new Date());

    console.log(this.upcomingBookings);
  }
}

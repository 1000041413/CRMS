import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthcredentialstoreService } from '../core/authentication/authcredentialstore.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']
})
export class BookingComponent implements OnInit {
  dtOptions: DataTables.Settings = {};

  constructor(private http: HttpClient, private router: Router, private toast: ToastrService, private authstore: AuthcredentialstoreService) { }
  bookings:any = [];

  public data:any;
  public filterQuery = '';
  public rowsOnPage = 10;
  public sortBy = 'id';
  public sortOrder = 'desc';
  isFacultyManager:any;
  userid:number = 0;

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
    this.isFacultyManager = this.authstore.isFacultyManager();
    this.userid = this.authstore.credentials!.userid;

    this.bindBookingsGrid();
  }

  bindBookingsGrid() {
    if (this.isFacultyManager) {
      this.http
        .get(environment.serverUrl + 'booking/getbookings')
        .subscribe((data) => {
          this.bookings = data;
        });
    } else {
      this.http
        .get(environment.serverUrl + 'booking/GetBookingsByUser?userId=' + this.userid)
        .subscribe((data) => {
          this.bookings = data;
        });
    }
  }

  refresh() {
    this.bookings = [];
    this.bindBookingsGrid();
  }

  cancelBooking(booking:any) {
    
      // Cancel Booking
      console.log(booking);
    
      this.http.put(environment.serverUrl + 'booking/cancel', booking).subscribe(res => {
        this.toast.success('Booking Cancelled!!');
        this.refresh();
      });
    
  }

  delete(deleteitem:any) {
    console.log(deleteitem);
    if (confirm('Are you sure to delete \'' + deleteitem.name + '\' ?')) {
      this.http
        .delete(environment.serverUrl + 'room/delete?id=' + deleteitem.id)
        .subscribe((data) => {
          this.toast.success('Deleted Successfully!!!');
          this.refresh();
        });
    }
  }

  public toInt(num: string) {
    return +num;
  }

  public sortByWordLength = (a: any) => {
    return a.city.length;
  }

}

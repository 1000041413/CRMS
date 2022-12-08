import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {
  dtOptions: DataTables.Settings = {};

  constructor(private http: HttpClient, private router: Router, private toast: ToastrService) { }
  roomslist:any = [];

  public data:any;
  public filterQuery = '';
  public rowsOnPage = 4;
  public sortBy = 'id';
  public sortOrder = 'desc';

  ngOnInit() {    
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
    this.bindRoomsGrid();
  }

  bindRoomsGrid() {
    this.http
      .get(environment.serverUrl + 'room/GetRooms')
      .subscribe((data) => {
        this.roomslist = data;
      });
  }

  refresh() {
    this.roomslist = [];
    this.bindRoomsGrid();
  }

  edit(room:any) {
    console.log(room);
    this.router.navigate(['/room/form', { roomId: room.Id }], { replaceUrl: true });
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


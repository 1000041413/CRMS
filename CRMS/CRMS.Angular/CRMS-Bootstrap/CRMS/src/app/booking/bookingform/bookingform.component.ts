import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { HttpClient} from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { AuthcredentialstoreService } from '../../core/authentication/authcredentialstore.service';
import { environment } from 'src/environments/environment';
 

@Component({
  selector: 'app-bookingform',
  templateUrl: './bookingform.component.html',
  styleUrls: ['./bookingform.component.scss']
  
})
export class BookingformComponent implements OnInit {

  roomList: any;
  bookingForm: FormGroup|any;
  isAddForm = true;
  mindate = new Date();
  errors = [];
  filteredOptions: any;
  roomControl: FormControl = new FormControl();
  filteredRoomList: any=[];


  constructor(private formBuilder: FormBuilder,
    private http: HttpClient,
    private toast: ToastrService,
    private router: Router, private route: ActivatedRoute, private authstore: AuthcredentialstoreService) { }

  ngOnInit() {
    this.createForm();
    this.initBindControls();
    this.roomControl.valueChanges.subscribe(newValue => {
      this.filteredRoomList = this.filter(newValue);
    });
  }

  filter(val: string): any[] {
    return this.roomList.filter((room:any) =>
      room.value.toLowerCase().indexOf(val.toString().toLowerCase()) > -1);
  }

  displayFn(room?: any): string | undefined {
    return room ? room.value : undefined;
  }

  formatDatetime(datestr:any, timestr:any) {
    const dateformated = moment(datestr).format('DD/MM/YYYY');
    // var timeformated = moment(timestr).format('HH:mm');
    const datetime = moment(dateformated + ' ' + timestr, 'DD/MM/YYYY HH:mm');
    return datetime.format('YYYY/MM/DD HH:mm');
  }


  save() {
    console.log(this.bookingForm.value);
    console.log(this.roomControl.value);
    const formvalue = this.bookingForm.value;
    const bookingData = {
      startDateTs: this.formatDatetime(formvalue.startDate, formvalue.startTime),
      endDateTs: this.formatDatetime(formvalue.endDate, formvalue.endTime),
      roomId: this.roomControl,
      userId: this.authstore.credentials!.userid,
      isCancelled:0,
      creationTs: new Date(),
      statusType:1,
      createdBy:this.authstore.credentials?.username
    };

    console.log(bookingData);
    this.http.post(environment.serverUrl+'booking/save', bookingData).subscribe((res:any) => {
      this.toast.success('Room booked Successfully!!!');
      this.router.navigate(['/booking'], { replaceUrl: true });
    }, (err:any) => {
      //const error = err.json();
      this.handleException(err);
      if (err.exceptionMessage.indexOf("Can't book a room for past time!")) {
         this.toast.error(err.exceptionMessage);
      }
    });
  }

  handleException(error:any) {
    if (error.error.exceptionMessage) {
      // Unhandled Exception
      this.toast.error(error.error.exceptionMessage);
    } else if (error.message) {
      // console.log(JSON.parse(error.text()));
      for (const propname of Object.keys(error.error.modelState)) {
        this.errors.push(error.error.modelState[propname] as never);
      }
    } else {
      console.log(error);
    }
  }

  onFormMouseLeave(event:any) {
    console.log(event);
    this.errors = [];
  }
  initBindControls() {
    this.http.get(environment.serverUrl+'room/GetRoomsLookup')
      .subscribe((data:any) => {
        this.roomList = data;
        console.log(this.roomList);
        this.filteredRoomList = this.roomList;

      });
  }

  private createForm() {
    this.bookingForm = this.formBuilder.group({
      id: 0,
      roomId: [null, [Validators.required]],
      startDate: [null, [Validators.required]],
      startTime: [null, [Validators.required]],
      endDate: [null, [Validators.required]],
      endTime: [null, [Validators.required]],
      creationTs: Date,
      createdBy: '',
      statusType: Number
    });
  }

  dateLessThan(from: string, to: string) {
    return (group: FormGroup): { [key: string]: any } => {
      const f = group.controls[from];
      const t = group.controls[to];
      if (f.value > t.value) {
        return {
          dates: 'Date from should be less than Date to'
        };
      }
      return {};
    };
  }


get roomId() {
  return this.bookingForm.get('roomId') as FormControl
}
get f() { return this.bookingForm.controls; }
}
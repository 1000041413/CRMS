import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthcredentialstoreService } from 'src/app/core/authentication/authcredentialstore.service';

 

@Component({
  selector: 'app-roomform',
  templateUrl: './roomform.component.html',
  styleUrls: ['./roomform.component.scss']
})
export class RoomformComponent implements OnInit {
  roomTypeList: any;
  roomForm: FormGroup| any = null;
  isAddForm = true;
  constructor(private formBuilder: FormBuilder,
    private http: HttpClient,
    private toast: ToastrService,
    private router: Router, private route: ActivatedRoute,  private authstore: AuthcredentialstoreService) { }

  ngOnInit() {
    this.createForm();
    this.initBindControls();

    this.route.params.subscribe(
      (params: Params) => {
        console.log(params);
        if (params && params.id) {
          this.isAddForm = false;
          this.roomForm.setValue({
            id: params.id,
            name: params.name,
            location: params.location,
            capacity: params.capacity,
            roomTypeId: params.roomTypeId,
            isActive: params.isActive,
            creationTs: params.creationTs,
            createdBy: params.createdBy,
            statusType: params.statusType,
            modifiedTs: new Date(),
            modifiedBy: this.authstore.credentials?.username
          });
        }

      }
    );

  }



  save() {
    console.log(this.roomForm.value);
    this.http.post(environment.serverUrl+'room/Save', this.roomForm.value).subscribe(res => {
      if (this.isAddForm) {
        this.toast.success('Added Successfully!!!');
      } else {
        this.toast.success('Updated Successfully!!!');
      }

      this.router.navigate(['/room'], { replaceUrl: true });
    });
  }

  initBindControls() {
    this.http.get(environment.serverUrl+'roomtype/GetRoomTypes')
      .subscribe((data:any) => {
        this.roomTypeList = data;
        console.log(this.roomTypeList);
      });
  }

  private createForm() {
    this.roomForm = this.formBuilder.group({
      id: 0,
      name: [null, [Validators.required]],
      location: [null, [Validators.required]],
      capacity: [null, [Validators.required]],
      roomTypeId: [null, [Validators.required]],
      isActive: [null, [Validators.required]],
      creationTs: new Date(),
      createdBy: this.authstore.credentials?.username,
       modifiedTs:'',
       modifiedBy: '',
       statusType: Number
    });
  }
}

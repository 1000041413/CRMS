import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../core/authentication/authentication.service';
import { AuthcredentialstoreService } from '../core/authentication/authcredentialstore.service';
// import { I18nService } from '../../i18n.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  menuHidden = true;
  isFacultyManager:any;

  constructor(private router: Router,
              private authenticationService: AuthenticationService, private authstore: AuthcredentialstoreService) { }

  ngOnInit() {
    this.isFacultyManager = this.authstore.isFacultyManager();
  }

  toggleMenu() {
    this.menuHidden = !this.menuHidden;
  }

  // setLanguage(language: string) {
  //   this.i18nService.language = language;
  // }

  logout() {
    this.authenticationService.logout()
      .subscribe(() => this.router.navigate(['/login'], { replaceUrl: true }));
  }

  // get currentLanguage(): string {
  //   return this.i18nService.language;
  // }

  // get languages(): string[] {
  //   return this.i18nService.supportedLanguages;
  // }

  get username(): string {
    const credentials = this.authstore.credentials;
    return credentials ? credentials.unique_name : '';
  }

  get userLoggedInUserName(): string {
    const credentials = this.authstore.credentials;
    return credentials ? credentials.username : '';
  }
}
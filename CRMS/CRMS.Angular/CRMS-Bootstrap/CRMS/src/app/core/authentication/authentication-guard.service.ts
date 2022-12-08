import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

import { AuthenticationService } from './authentication.service';
import { AuthcredentialstoreService } from './authcredentialstore.service';

@Injectable({'providedIn':'root'})
export class AuthenticationGuardService implements CanActivate {

  constructor(private router: Router,
    private authStore: AuthcredentialstoreService) { }

    canActivate(): boolean {
      if (this.authStore.isAuthenticated()) {
        return true;
      }
  
      // log.debug('Not authenticated, redirecting...');
      this.router.navigate(['/login'], { replaceUrl: true });
      return false;
    }  
}

import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { PainelApiService } from './painel-api.service';

@Injectable({
  providedIn: 'root'
})
export class RotaLogadaGuard implements CanActivate {

  constructor(private service: PainelApiService, private router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (!this.service.isLogado) {
      return this.router.parseUrl('/login');
    }
    return this.service.isLogado;
  }

}

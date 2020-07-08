import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PainelComponent } from './painel/painel.component';
import { LoginComponent } from './login/login.component';
import { RotaLogadaGuard } from './rota-logada.guard';


const routes: Routes = [
  { path: 'painel', component: PainelComponent, canActivate: [RotaLogadaGuard] },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '/painel', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

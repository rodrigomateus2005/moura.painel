import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PainelComponent } from './painel/painel.component';
import { LoginComponent } from './login/login.component';
import { Select2Directive } from './directives/select2.directive';
import { HttpClientModule } from '@angular/common/http';
import { CarregandoComponent } from './components/carregando/carregando.component';
import { MaterializeModalDirective } from './directives/materialize-modal.directive';

@NgModule({
  declarations: [
    AppComponent,
    PainelComponent,
    LoginComponent,
    Select2Directive,
    CarregandoComponent,
    MaterializeModalDirective
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

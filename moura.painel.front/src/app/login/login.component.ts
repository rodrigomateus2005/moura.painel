import { Component, OnInit } from '@angular/core';

import * as M from 'materialize-css';
import { Router } from '@angular/router';
import { PainelApiService } from '../painel-api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public usuario: string;
  public senha: string;

  constructor(private router: Router, private service: PainelApiService) { }

  ngOnInit(): void {
  }

  logar() {
    if (!this.usuario) {
      M.toast({html: 'Informe o usuário'});
      return;
    }

    if (!this.senha) {
      M.toast({html: 'Informe a senha'});
      return;
    }

    this.service.logar(this.usuario, this.senha).then(response => {
      this.router.navigate(['painel']);
    }).catch(err => {
      M.toast({html: 'Usuário ou senha incorretos'});
    });
  }

}

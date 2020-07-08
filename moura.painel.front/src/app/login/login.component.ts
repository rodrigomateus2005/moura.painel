import { Component, OnInit } from '@angular/core';

import * as M from 'materialize-css';
import { Router } from '@angular/router';
import { PainelApiService } from '../painel-api.service';
import { CarregandoService } from '../components/carregando/carregando.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public usuario: string;
  public senha: string;

  constructor(private router: Router, private service: PainelApiService, private carregandoService: CarregandoService) { }

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

    const carregando = this.carregandoService.getInstance();

    carregando.abrir();

    this.service.logar(this.usuario, this.senha).then(response => {
      carregando.fechar();
      this.router.navigate(['painel']);
    }).catch(err => {
      carregando.fechar();
      M.toast({html: 'Usuário ou senha incorretos'});
    });
  }

}

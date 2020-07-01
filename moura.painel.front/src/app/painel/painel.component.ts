import { Component, OnInit } from '@angular/core';
import { PainelApiService, Portal } from '../painel-api.service';
import { HttpClient } from '@angular/common/http';

import * as $ from 'jquery';
import { CarregandoService } from '../components/carregando/carregando.service';

@Component({
  selector: 'app-painel',
  templateUrl: './painel.component.html',
  styleUrls: ['./painel.component.scss']
})
export class PainelComponent implements OnInit {

  public linkEmpresa: string;

  public portais: Portal[];

  public get portalNomes(): string[] {
    if (!this.portais) {
      return null;
    }

    return this.portais.map(x => x.nome);
  }

  public empresaNomes(portal: Portal): string[] {
    return portal.empresas.map(x => x.empresa.fantasia);
  }

  constructor(private service: PainelApiService, private carregandoService: CarregandoService) { }

  ngOnInit(): void {
    const carregando = this.carregandoService.getInstance();

    carregando.abrir();

    this.service.getPortais().then((result) => {
      this.portais = result;

      if (this.portais && this.portais.length > 0) {
        this.linkEmpresa =  this.portais[0].link;
      }

      carregando.fechar();
    }).catch((err) => {
    });
  }

  abrirLink() {
    this.service.abrirPortal(this.linkEmpresa);
    // $.get(this.linkEmpresa).then((e) => {
    //   $.post(this.linkEmpresa + '/api/Login/Login', {
    //         usuario: 'moura',
    //         senha: '886648',
    //         empresaSelecionada: 0
    //       }).then((result) => {
    //         console.log(result);

    //         window.location.href = this.linkEmpresa;

    //       }).catch((err) => {
    //         console.error(err);
    //       });
    // }).catch((e) => {
    //   console.error(e);
    // });
  }

}

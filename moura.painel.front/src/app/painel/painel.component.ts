import { Component, OnInit } from '@angular/core';
import { PainelApiService, Portal, PortalEmpresa } from '../painel-api.service';
import { HttpClient } from '@angular/common/http';

import { CarregandoService } from '../components/carregando/carregando.service';

@Component({
  selector: 'app-painel',
  templateUrl: './painel.component.html',
  styleUrls: ['./painel.component.scss']
})
export class PainelComponent implements OnInit {

  public empresa: PortalEmpresa;

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
    this.service.getPortais().then((result) => {
      this.portais = result;

      if (this.portais && this.portais.length > 0 && this.portais[0].empresas.length > 0) {
        this.empresa =  this.portais[0].empresas[0];
      }
    }).catch((err) => {
    });
  }

  abrirLink() {
    const portal = this.empresa.portal;

    if (portal) {
      this.service.abrirPortal(portal.link, this.empresa.codigo);
    }
  }

}

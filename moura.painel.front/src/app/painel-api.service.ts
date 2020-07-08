import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { APP_BASE_HREF, PlatformLocation } from '@angular/common';

import * as $ from 'jquery';
import { CarregandoService } from './components/carregando/carregando.service';

export interface Login {
  usuario: string;
  senha: string;
  paineis: Portal[];
}

export interface Empresa {
  codigo: number;
  razao_Social: string;
  fantasia: string;
}

export interface PortalEmpresa {
  codigo: number;
  portal: Portal;
  empresa: Empresa;
}

export interface Portal {
  codigo: number;
  nome: string;
  link: string;
  empresas: PortalEmpresa[];
}

@Injectable({
  providedIn: 'root'
})
export class PainelApiService {

  private urlApi = 'https://localhost:44336/';

  private bearerToken: string;
  private login: Login;

  public get isLogado(): boolean {
    if (this.bearerToken) {
      return true;
    }
    return false;
  }

  constructor(private http: HttpClient, platformLocation: PlatformLocation, private carregandoService: CarregandoService) {
    this.urlApi = '';
  }

  public logar(usuario: string, senha: string): Promise<Login> {
    return new Promise<Login>((resolve, reject) => {
      this.http.post<Login>(this.urlApi + 'api/Painel/Login', {
        Usuario: usuario,
        Senha: senha
      },
      {observe: 'response'}).toPromise().then((result) => {
        if (result.ok) {
          this.bearerToken = result.headers.get('X-Token');
          this.login = result.body;
          resolve(result.body);
        } else {
          reject('Usuário ou senha incorretos');
        }
      }).catch((err) => {
        reject(err);
      });
    });
  }

  public getPortais(): Promise<Portal[]> {
    for (const portal of this.login.paineis) {
      for (const empresa of portal.empresas) {
        empresa.portal = portal;
      }
    }

    return Promise.resolve(this.login.paineis);

    // return Promise.resolve([
    //   {
    //     codigo: 1,
    //     nome: 'PORTAL 6051 - Lupo - Franqueadora',
    //     link: 'http://8142dd3c3978.ngrok.io/',
    //     empresas: [
    //       {
    //         codigo: 1,
    //         empresa: {
    //           codigo: 101,
    //           razao_Social: 'CARLOS ALBERTO MAZZEU',
    //           fantasia: 'SHOPPING LUPO'
    //         }
    //       }
    //     ]
    //   }
    // ]);
  }

  public abrirPortal(link: string, empresa: number) {
    const carregando = this.carregandoService.getInstance();
    carregando.abrir();

    $.get(link).then((e) => {
      $.post(link + '/api/Login/Login', {
            usuario: this.login.usuario,
            senha: this.login.senha,
            empresaSelecionada: empresa
          }).then((result) => {
            carregando.fechar();
            window.location.href = link + '?X-Token=' + result.token;
          }).catch((err) => {
            carregando.fechar();
            console.error(err);
          });
    }).catch((e) => {
      carregando.fechar();
      console.error(e);
    });
  }

}

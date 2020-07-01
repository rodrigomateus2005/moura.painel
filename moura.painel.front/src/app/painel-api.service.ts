import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { APP_BASE_HREF, PlatformLocation } from '@angular/common';

export interface Empresa {
  codigo: number;
  razao_Social: string;
  fantasia: string;
}

export interface PortalEmpresa {
  codigo: number;
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

  public get isLogado(): boolean {
    if (this.bearerToken) {
      return true;
    }
    return false;
  }

  constructor(private http: HttpClient, platformLocation: PlatformLocation) {
    // this.urlApi = (platformLocation as any).location.origin;
    // console.log(this.urlApi);
    // console.log((platformLocation as any).location);

    this.urlApi = '';
  }

  public logar(usuario: string, senha: string): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      this.http.post(this.urlApi + 'api/Painel/Login', {
        Usuario: usuario,
        Senha: senha
      }, { responseType: 'text' }).toPromise().then((result) => {
        if (result) {
          this.bearerToken = result;
          resolve(true);
        } else {
          reject('Usuário ou senha incorretos');
        }
      }).catch((err) => {
        reject(err);
      });
    });
  }

  public getPortais(): Promise<Portal[]> {
    return new Promise<Portal[]>((resolve, reject) => {
      this.http.get<Portal[]>(this.urlApi + 'api/Painel/RetornarEmpresas', {
        headers: {
          Authorization: `Bearer ${this.bearerToken}`
        }
      }).toPromise().then((result) => {
        resolve(result);
      }).catch((err) => {
        reject(err);
      });
    });

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

  public abrirPortal(link: string) {
    // this.http.get(link).toPromise().then((result) => {
    //   console.log(result);
    // }).catch((err) => {
    // });

    window.location.href = link;
  }

}

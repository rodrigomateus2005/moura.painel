import { Injectable, ApplicationRef, ComponentFactoryResolver, ComponentRef, ViewContainerRef, Injector } from '@angular/core';
import { CarregandoComponent } from './carregando.component';

export interface ICarregandoRef {

  abrir(text?: string): Promise<void>;
  fechar(): void;
  mudarText(text: string): void;

}

@Injectable({
  providedIn: 'root'
})
export class CarregandoService {

  private carregandoComponentInstance: ComponentRef<CarregandoComponent>;

  constructor(
    private applicationRef: ApplicationRef,
    private componentFactoryResolver: ComponentFactoryResolver) {
  }

  public getInstance(): ICarregandoRef {
    if (!this.carregandoComponentInstance) {

      const componentFactory = this.componentFactoryResolver.resolveComponentFactory(CarregandoComponent);
      const vcr: ViewContainerRef = this.applicationRef.components[0].injector.get(ViewContainerRef);
      this.carregandoComponentInstance = vcr.createComponent(componentFactory);

      return new CarregandoRefMaster(this.carregandoComponentInstance.instance, this);
    } else {
      return new CarregandoRef(this.carregandoComponentInstance.instance);
    }
  }

  public clearInstance(): void {
    this.carregandoComponentInstance.destroy();
    this.carregandoComponentInstance = null;
  }

}

class CarregandoRef implements ICarregandoRef {

  constructor(protected component: CarregandoComponent) {
  }

  mudarText(text: string): void {
    this.component.mensagem = text;
  }

  abrir(text?: string): Promise<void> {
    if (text) {
      this.mudarText(text);
    }
    return null;
  }

  fechar(): void {
  }

}

class CarregandoRefMaster extends CarregandoRef {

  constructor(protected component: CarregandoComponent, private service: CarregandoService) {
    super(component);
  }

  abrir(text?: string): Promise<void> {
    super.abrir(text);
    return this.component.abrir();
  }

  fechar(): void {
    super.fechar();
    this.component.fechar();
    this.service.clearInstance();
  }

}



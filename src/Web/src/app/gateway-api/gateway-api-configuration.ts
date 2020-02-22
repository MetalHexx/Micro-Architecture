/* tslint:disable */
import { Injectable } from '@angular/core';

/**
 * Global configuration for GatewayApi services
 */
@Injectable({
  providedIn: 'root',
})
export class GatewayApiConfiguration {
  rootUrl: string = '';
}

export interface GatewayApiConfigurationInterface {
  rootUrl?: string;
}

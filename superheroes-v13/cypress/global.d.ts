/// <reference types="cypress" />

declare namespace Cypress {
  interface Chainable {
    getCommand(url: string, responceBody: Array<any>): Chainable<any>;
    deleteCommand(url: string): Chainable<any>;
    postCommand(url: string, requestBody: any): Chainable<any>;
  }
}

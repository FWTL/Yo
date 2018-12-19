'use strict';
const Generator = require('yeoman-generator');
const Guid = require('uuid/v4');

module.exports = class extends Generator {
  async initializing() {
    this.answers = {};
    this.answers.solutionGuid = Guid();
    this.answers.ApiGuid = Guid();
    this.answers.CoreGuid = Guid();
    this.answers.DatabaseGuid = Guid();
    this.answers.InfrastructureGuid = Guid();

    this.destinationRoot("result");

    var a = 5;
    var b = 10;
    console.log(`Fifteen is ${a + b} and\nnot ${2 * a + b}.`);
  }

  async prompting() {
    let prompts = await this.prompt([{
      type: 'input',
      name: 'solutionName',
      message: 'Solution name',
      default: "Solution",
    },
    {
      type: 'input',
      name: 'entityName',
      message: 'Entity name',
      default: "Foo"
    }]);
    Object.assign(this.answers, prompts);
  }

  writing() {
    //Copy root
    this.fs.copy(`${this.templatePath()}/*`, this.destinationPath());
    this.fs.copy(`${this.templatePath()}/dotfiles/gitattributes`, `${this.destinationPath()}/.gitattributes`);
    this.fs.copy(`${this.templatePath()}/dotfiles/gitignore`, `${this.destinationPath()}/.gitignore`);

    this.fs.copy(`${this.templatePath()}/_Template.Api/*`, `${this.destinationPath()}/_Template.Api`);
  }

  install() {
  }
};

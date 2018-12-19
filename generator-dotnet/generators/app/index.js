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
    this.fs.delete(this.destinationRoot());
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
    this.fs.copy(this.templatePath(),this.destinationPath());
  }

  install() {
  }
};

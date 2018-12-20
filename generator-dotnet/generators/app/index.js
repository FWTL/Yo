'use strict';
const Generator = require('yeoman-generator');
const Guid = require('uuid/v4');
const del = require('del');
const rename = require("gulp-rename");

module.exports = class extends Generator {
  async initializing() {
    this.answers = {};
    this.answers.SolutionGuid = Guid();
    this.answers.ApiGuid = Guid();
    this.answers.CoreGuid = Guid();
    this.answers.DatabaseGuid = Guid();
    this.answers.InfrastructureGuid = Guid();

    await del("result/*");
    this.destinationRoot("result");
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

    var _this = this;
    this.registerTransformStream(rename(function (path) {
      if (path.dirname.includes('_Template')) {
        path.dirname = path.dirname.replace('_Template', _this.answers.solutionName);
      }

      if (path.basename.includes('_Template')) {
        path.basename = path.basename.replace('_Template', _this.answers.solutionName);
      }

      if (path.basename.includes('_EntityName')) {
        path.basename = path.basename.replace('_EntityName', _this.answers.entityName);
      }
    }));

    //Copy root
    this.fs.copyTpl(`${this.templatePath()}/*`, this.destinationPath(), this.answers);
    this.fs.copy(`${this.templatePath()}/dotfiles/gitattributes`, `${this.destinationPath()}/.gitattributes`);
    this.fs.copy(`${this.templatePath()}/dotfiles/gitignore`, `${this.destinationPath()}/.gitignore`);

    this.fs.copyTpl(`${this.templatePath()}/_Template.Api/`, `${this.destinationPath()}/_Template.Api`, this.answers);
    this.fs.copyTpl(`${this.templatePath()}/_Template.Core/`, `${this.destinationPath()}/_Template.Core`, this.answers);
    this.fs.copyTpl(`${this.templatePath()}/_Template.Database/`, `${this.destinationPath()}/_Template.Database`, this.answers);
    this.fs.copyTpl(`${this.templatePath()}/_Template.Infrastructure/`, `${this.destinationPath()}/_Template.Infrastructure`, this.answers);
  }

  end() {
    
  }
};

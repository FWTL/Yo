'use strict';
const Generator = require('yeoman-generator');
const chalk = require('chalk');
const yosay = require('yosay');

module.exports = class extends Generator {
  initializing() {
    this.fs.delete("test");
  }

  async prompting() {
    this.answers = await this.prompt([{
      type: 'confirm',
      name: 'cool',
      message: 'Would you like to enable the Cool feature?'
    }]);
  }

  writing() {
    this.log('cool feature', this.answers.cool); // user answer `cool` used
  }

  install() {
  }
};

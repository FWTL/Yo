const Generator = require('yeoman-generator');

module.exports = class extends Generator {
    constructor(args, opts) {
        super(args, opts);
    }

    initializing() {
        this.log.apply("ASdasda");
        this.fs.delete("target");
    }
} 
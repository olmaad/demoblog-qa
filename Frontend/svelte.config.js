// svelte.config.js
const preprocess = require('my-example-svelte-preprocessor');

module.exports = {
    preprocess: [preprocess()],
    // ...other svelte options
};
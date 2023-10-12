module.exports = function override(config, env) {
    // Consolidate chunk files instead
    config.optimization.splitChunks = {
        cacheGroups: {
            default: false,
        },
    };
    // Move runtime into bundle instead of separate file
    config.optimization.runtimeChunk = false;


    
    // CSS
    const cssplugin = config.plugins.find(x => x.constructor.name === 'MiniCssExtractPlugin');
    cssplugin.options.filename = 'static/css/[name].css';

    // JS
    config.output.filename = 'static/js/[name].js';
    return config;
}
const path = require('path');
module.exports = {
    entry: ['./Scripts/es6/main.js'],
    output: {
        path: path.resolve(__dirname, './Scripts/build'),
        filename: 'bundle.js'
    },
    module: {
        rules: [{
            loader: 'babel-loader',
            test: /\.js$/,
            exclude: /node_modules/
        },
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    'css-loader'
                ]
            },
            {
                test: /\.(glsl|frag|vert)$/,
                exclude: /node_modules/,
                loader: 'glslify-import-loader'
            },
            {
                test: /\.(glsl|frag|vert)$/,
                exclude: /node_modules/,
                loader: 'raw-loader'
            },
            {
                test: /\.(glsl|frag|vert)$/,
                exclude: /node_modules/,
                loader: 'glslify-loader'
            }]
    }
}
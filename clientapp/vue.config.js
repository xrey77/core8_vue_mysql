const { defineConfig } = require('@vue/cli-service')

module.exports = defineConfig({
  transpileDependencies: true,
  pages: {
    index: {
      entry: 'src/main.ts',
      title: 'Diebold-Nixdorf',
    },
  },
  configureWebpack: {
    performance: {
      hints: false, // or "warning"
      maxEntrypointSize: 1024000,
      maxAssetSize: 1024000
    },    
    optimization: {
      splitChunks: {
        chunks: 'all',
        maxInitialRequests: Infinity,
        minSize: 20000,
        cacheGroups: {
          vendor: {
            test: /[\\/]node_modules[\\/]/,
            name(module) {
              if (!module.context) return 'vendor-unknown';

              // 2. Perform the match
              const match = module.context.match(/[\\/]node_modules[\\/](.*?)([\\/]|$)/);
              
              // 3. If no match is found, provide a fallback name
              if (!match) return 'vendor-others';

              // 4. Safely access index [1]
              const packageName = match[1];
              return `npm.${packageName.replace('@', '')}`;
            },
          },
        },
      },
    },
  },
})

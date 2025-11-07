git branch -D production
git checkout --orphan production
rm WebBuild -r
mkdir WebBuild
godot --path . --export-release "Web" ./WebBuild/index.html --headless
git rm -rf .
cp -r WebBuild/* .
git add .
git commit -m "Web production"
git push https://github.com/tab-software/B.U.B.B.L.E/ production --force
git checkout main
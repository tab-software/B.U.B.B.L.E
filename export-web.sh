git branch -D production
git checkout --orphan production
rm ./../WebBuild -r -f
mkdir ./../WebBuild
godot --path . --export-release "Web" ./../WebBuild/index.html --headless
rf * -r -f
cp -r ./../WebBuild/* .
rm ./../WebBuild -r -f
git add .
git commit -m "Web production"
git push https://github.com/tab-software/B.U.B.B.L.E/ production --force
git checkout main
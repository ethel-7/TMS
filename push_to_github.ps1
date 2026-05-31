# Check GitHub CLI authentication status
Write-Host "=== TMS GitHub Deployment Automator ===" -ForegroundColor Cyan

$ghPath = "C:\Program Files\GitHub CLI\gh.exe"
if (-not (Test-Path $ghPath)) {
    $ghPath = "gh"
}

Write-Host "Checking GitHub CLI authentication status..." -ForegroundColor Yellow
$authStatus = & $ghPath auth status 2>&1

if ($authStatus -match "You are not logged into any GitHub hosts" -or $LASTEXITCODE -ne 0) {
    Write-Host "Authentication required. Launching GitHub CLI login..." -ForegroundColor Yellow
    Write-Host "Please select 'GitHub.com', select 'HTTPS', select 'Yes' for Git credentials, and select 'Login with a web browser'." -ForegroundColor White
    & $ghPath auth login
} else {
    Write-Host "Already authenticated with GitHub!" -ForegroundColor Green
}

# Create the remote repository
Write-Host ""
Write-Host "Creating remote repository 'TMS' on GitHub..." -ForegroundColor Yellow
& $ghPath repo create TMS --public --source=. --push

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ GitHub repository created successfully!" -ForegroundColor Green
} else {
    Write-Host "Repository might already exist or creation failed. Proceeding to push branches..." -ForegroundColor DarkYellow
}

# Push all branches and tags
Write-Host ""
Write-Host "Pushing all local feature branches and tags to remote origin..." -ForegroundColor Yellow
git push origin --all
git push origin --tags

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "=== TMS Platform successfully deployed to GitHub! ===" -ForegroundColor Green
    Write-Host "Visit your repository to verify all branches and release tags are online." -ForegroundColor Green
} else {
    Write-Host ""
    Write-Host "Deployment completed with some warnings/errors. Please check the logs above." -ForegroundColor Red
}

{
  description = "CSharp/Unity flake";
  inputs = {
    nixpkgs.url = "nixpkgs";
  };
  outputs =
    {
      nixpkgs,
      ...
    }:
    let
      system = "x86_64-linux";
      pkgs = import nixpkgs {
        inherit system;
        config.allowUnfree = true;
      };
    in
    {
      devShells.${system}.default = pkgs.mkShellNoCC {
        shellHook = ''
          export FrameworkPathOverride=${pkgs.mono}/lib/mono/4.8-api
        '';
      };
    };
}
